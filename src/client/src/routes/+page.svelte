<script>
    import { onMount } from "svelte";
    import GetCategory from "$lib/components/CategoryTable.svelte";
    import GetEvent from "$lib/components/ListEvents.svelte";
    import CreateEvent from "$lib/components/CreateEvent.svelte";
    import DateTimeInput from "$lib/components/date-timeInput.svelte";

</script>

<h1>Welcome to SvelteKit</h1>

<a href="/createCategory">Category</a>

<div>
    <CreateEvent></CreateEvent>
    
    <GetEvent></GetEvent>
    
    
</div>

