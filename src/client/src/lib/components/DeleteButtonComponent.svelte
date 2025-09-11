<script lang="ts">
  import ApiService from '$lib/services/apiService';
  import { singularize } from '$lib/utils/singularize';

  export let resource: string; // e.g. "categories", "events"
  export let id: number;
  export let className: string;

  const apiService = new ApiService();

  async function handleDelete(event: MouseEvent) {
    event.stopPropagation();
    
    const singular = singularize(resource);

    if (confirm(`Are you sure you want to delete this ${singular}?`)) {
      const result = await apiService.delete(`${resource}/${id}`);

      if (typeof result === 'object' && result !== null && "code" in result) {
        const errorResult = result as { code: number; message: string };
        alert(`Error deleting: ${errorResult.message}`);
      } else {
        const deletedEvent = new CustomEvent('deleted', {
          detail: { id, resource },
          bubbles: true,
          composed: true
        });
        dispatchEvent(deletedEvent);
      }
    }
  }
</script>

<button class={className} on:click={handleDelete} title="Delete">
  Delete
</button>