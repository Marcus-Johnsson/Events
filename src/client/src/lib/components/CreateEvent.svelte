<script lang="ts">
    import type {EventData} from "$lib/services/event/eventServicePost"
    import {EventPostService} from "$lib/services/event/eventServicePost"
    import DateTimeInput from "$lib/components/date-timeInput.svelte"
    import ApiService from "$lib/services/apiService";
    import { onMount } from "svelte";
    import { CategoryPostService } from "$lib/services/category/GetCategory";

    const apiService = new ApiService();
    const eventPostService = new EventPostService(apiService);
    
      interface Category{
        title: string;
        id: number;
    }
 
      let categories = $state<Category[]>([]);
          onMount(async () => {
      categories = await new CategoryPostService(apiService).GetCategories();
      categories.sort((a: Category, b: Category) => b.id - a.id );
      });

    let title = $state("");
    let description = $state("");
    let location = $state("");
    let selectedCategories: string[] = $state([]);
    let dateStart: Date = $state(new Date());
    let dateEnd: Date = $state(new Date());

async function createEvent() {
    const eventData: EventData = {
        title,
        description,
        location,
        categories: selectedCategories,
        dateStart,
        dateEnd
    };

    await eventPostService.postEvent(eventData);
    console.log("Event sent:", eventData);
}

</script>
    <DateTimeInput bind:startDateTime={dateStart} bind:endDateTime={dateEnd} />

<div>
    <select multiple bind:value={selectedCategories}>
    {#each categories as category}
        <option value={category.id}>{category.title}</option>
    {/each}
</select>

    <input type="text" bind:value={title} placeholder="Title">
    <input type="text" bind:value={description} placeholder="Description">
    <input type="text" bind:value={location} placeholder="Location">


    <button onclick={createEvent}>Create</button>


  


</div>