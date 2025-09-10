<script>
    import { onMount } from "svelte";
    import GetCategory from "$lib/components/getCategoryTable.svelte";
</script>

<h1>Welcome to SvelteKit</h1>

<a href="/createCategory">Category</a>

<div>
    <GetCategory />
</div>
