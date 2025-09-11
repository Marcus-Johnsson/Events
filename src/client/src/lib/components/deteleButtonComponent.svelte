<script lang="ts">
  import ApiService from '$lib/services/apiService';
  export let resource: string; // e.g. "categories", "events"
  export let id: number;

  const apiService = new ApiService();

  async function handleDelete(event: MouseEvent) {
    event.stopPropagation();
    if (confirm(`Are you sure you want to delete this ${resource.slice(0, -1)}?`)) {
      const result = await apiService.delete(`${resource}/${id}`);
      if (typeof result === 'object' && result !== null && "code" in result) {
        const errorResult = result as { code: number; message: string };
        alert(`Error deleting: ${errorResult.message}`);
      } else {
        const deletedEvent = new CustomEvent('deleted', {
          detail: { id, resource },
          bubbles: true
        });
        dispatchEvent(deletedEvent);
        window.location.reload();
      }
    }
  }
</script>

<button on:click={handleDelete} title="Delete" style="font-size: 1em; padding: 0.2em 0.8em;">
  Delete
</button>