<script>
    import { onMount } from "svelte";
    import GetCategory from "$lib/components/CategoryTable.svelte";
    import GetEvent from "$lib/components/ListEvents.svelte";
    
</script>

<h1>Welcome to SvelteKit</h1>

<a href="/createCategory">Category</a>

<div>
    <GetCategory />
    <GetEvent />
</div>

