<script lang="ts">
    import type {EventData} from "$lib/services/event/eventServicePost"

let eventData: EventData = {
    name: "",
    dateStart: new Date(),
    dateEnd: new Date(),
    location: "",
    description: "",
    cateories: [] // empty array is fine
};

</script>


<div>
    <input type="search" bind:value={eventData.cateories} placeholder="Categori">
    <input type="text" bind:value={eventData.name} placeholder="Title">
    <input type="text" bind:value={eventData.description} placeholder="Description">
    <input type="text" bind:value={eventData.location} placeholder="Location">
    <input type="date" bind:value={eventData.dateStart} placeholder="Event start">
    <input type="date" bind:value={eventData.dateEnd} placeholder="Event ends"/>

</div>
