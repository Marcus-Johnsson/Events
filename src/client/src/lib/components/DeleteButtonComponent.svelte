<script lang="ts">
  export let onDelete: (id: number) => Promise<any>;
  export let itemId: number;
  export let itemType: string = "Item";
  export let className: string;

  // Debug logging
  console.log('DeleteButtonComponent props:', { onDelete, itemId, itemType, className });
  console.log('onDelete type:', typeof onDelete);

  async function handleDelete(event: MouseEvent) {
    event.stopPropagation();
    console.log('Delete button clicked:', { itemId, itemType, onDelete });
    if (confirm(`Are you sure you want to delete this ${itemType}?`)) {
      try {
        await onDelete(itemId);
        const deletedEvent = new CustomEvent('deleted', {
          detail: { id: itemId, type: itemType },
          bubbles: true,
          composed: true
        });
        dispatchEvent(deletedEvent);
      } catch (err) {
        const errorMessage = typeof err === 'object' && err !== null && 'message' in err ? (err as { message: string }).message : String(err);
        alert(`Error deleting: ${errorMessage}`);
      }
    }
  }
</script>

<button class={className} on:click={handleDelete} title={`Delete ${itemType}`}>
  Delete {itemType}
</button>