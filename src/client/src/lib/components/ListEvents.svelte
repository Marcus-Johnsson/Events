<script lang="ts">
    import ApiService from "$lib/services/apiService";
    import { EventServiceGet } from '$lib/services/event/eventServiceGet'
    import { onMount } from "svelte";
    import type { GetAllEventResponse } from '$lib/services/event/eventServiceGet';
    import DeleteButtonComponent from "$lib/components/DeteleButtonComponent.svelte";

    const apiService = new ApiService();
    const eventGetService = new EventServiceGet(apiService);
    export let data: { events: GetAllEventResponse[] } = { events: [] };
   
    

    onMount(async () => {
        data.events = await eventGetService.getAllEvents();
    });

    function handleDeleted(event: CustomEvent<{ id: number }>) {
          const deletedId = event.detail.id;
          data.events = data.events.filter(event => event.id !== deletedId);
      }

</script>

<h1>All Events</h1>



<div class="card-container">
    {#each data.events as event}
        <div class="card">
            <h2>{event.title}</h2>
            <p class="description">{event.description}</p>
            <p><strong>Plats:</strong> {event.location}</p>
            <p><strong>Start:</strong> {new Date(event.startDate).toLocaleString()}</p>
            <p><strong>Slut:</strong> {new Date(event.endDate).toLocaleString()}</p>
            <p><strong>Kategorier:</strong> {event.categories?.map(c => c.title).join(", ")}</p>
            
            <div class="actions">
                <button>Ändra</button>
                <DeleteButtonComponent resource="events" id={event.id} on:deleted={handleDeleted} />
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

    .danger {
        background: #e74c3c;
        color: white;
    }
</style>
