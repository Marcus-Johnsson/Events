<script lang="ts">
    import ApiService from "$lib/services/apiService";
    import { EventServiceGet } from '$lib/services/event/eventServiceGet'
    import { onMount } from "svelte";
    import type { GetAllEventResponse } from '$lib/services/event/eventServiceGet';
    


    const apiService = new ApiService();
    const eventGetService = new EventServiceGet(apiService);
    export let data: { events: GetAllEventResponse[] } = { events: [] };
   
    

    onMount(async () => {
        data.events = await eventGetService.getAllEvents();
    });

</script>

<h1>All Events</h1>

<ul>
    {#each data.events as event}
        <li class="mb-2 p-2 border-b">
            <h2 class="font-semibold">{event.title}</h2>
            <p>{event.description}</p>
            <ul>Location: {event.location}</ul>
            <ul>Start: {new Date(event.startDate).toLocaleString()}</ul>
            <ul>End: {new Date(event.endDate).toLocaleString()}</ul>
            <p>Categories: {event.categories?.map(c => c.title).join(", ")}</p>
        </li>
    {/each}
</ul>

