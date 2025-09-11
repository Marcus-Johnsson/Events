<script>
    import { onMount } from "svelte";
    import GetCategory from "$lib/components/getCategoryTable.svelte";
    import GetEvent from "$lib/components/getEvent.svelte";
    import CreateEvent from "$lib/components/CreateEvent.svelte";

</script>

<h1>Welcome to SvelteKit</h1>

<a href="/createCategory">Category</a>

<div>
    <CreateEvent></CreateEvent>

    <GetEvent></GetEvent>    
</div>

