<script lang="ts">
    import ApiService from "$lib/services/apiService";
    import { EventServiceGet } from '$lib/services/event/eventServiceGet'
    import { onMount } from "svelte";
    import type { GetAllEventResponse } from '$lib/services/event/eventServiceGet';
    import DeleteButtonComponent from "$lib/components/DeleteButtonComponent.svelte";
    import { EventDeleteService } from '$lib/services/event/eventServiceDelete';

    const apiService = new ApiService();
    const eventGetService = new EventServiceGet(apiService);
    const eventDeleteService = new EventDeleteService(apiService);
    export let data: { events: GetAllEventResponse[] } = { events: [] };

    onMount(async () => {
        data.events = await eventGetService.getAllEvents();
    });

    async function handleDeleteEvent(id: number) {
      try {
        await eventDeleteService.deleteEvent({ id });
        data.events = data.events.filter(e => e.id !== id);
      } catch (err) {
        alert(`Error deleting event: ${(err as any)?.message || err}`);
      }
    }
</script>

<h1>All Events</h1>



<div class="card-container">
    {#each data.events as event}
        <div class="card">
            <img src="https://media.tenor.com/yheo1GGu3FwAAAAM/rick-roll-rick-ashley.gif" alt="Event Image" />
            <h2>{event.title}</h2>
            <p class="description">{event.description}</p>
            <p><strong>Plats:</strong> {event.location}</p>
            <p><strong>Start:</strong> {new Date(event.startDate).toLocaleString()}</p>
            <p><strong>Slut:</strong> {new Date(event.endDate).toLocaleString()}</p>
            <p><strong>Kategorier:</strong> {event.categories?.map(c => c.title).join(", ")}</p>
            
            <div class="actions">
                <button>Ändra</button>
                <DeleteButtonComponent
                onDelete={handleDeleteEvent}
                itemId={event.id}
                itemType="Event"
                className="deleteEventButton"
              />
            </div>
        </div>
    {/each}
</div>

<style>
    .card-container {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 1rem;
    }

    .card {
        background: white;
        border-radius: 8px;
        padding: 1rem;
        box-shadow: 0 2px 6px rgba(0,0,0,0.1);
        display: flex;
        flex-direction: column;
        justify-content: space-between;
    }

    .card h2 {
        margin-top: 0;
        font-size: 1.2rem;
    }

    .card img {
        width: 200px;       /* fast bredd */
        height: 200px;      /* fast höjd */
        object-fit: cover;  /* ser till att bilden inte blir ihoptryckt */
        border-radius: 6px;
    }

    .card .description {
        color: #555;
        font-size: 0.9rem;
    }

    .actions {
        margin-top: 1rem;
        display: flex;
        gap: 0.5rem;
    }

    button {
        padding: 0.5rem 1rem;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

    :global(.deleteEventButton) {
        padding: 0.5rem 1rem;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

    :global(.deleteEventButton):hover {
        background: #b62525;
        color: white;
    }
</style>
