<script>
    import ApiService from '$lib/services/apiService';
    import { CategoryPostService } from '$lib/services/category/PostCategory';

    
    const apiService = new ApiService();
    const categoryPostService = new CategoryPostService(apiService);
    let title = ""; // bind to input field

    async function createCategory() {
        if (!title.trim()) {
            alert("Please enter a category name.")
            return;
        }

        await categoryPostService.postCategory(title);
        title = ""; // clear input after posting
    }
    </script>
<h1>Create Category</h1>
<p>This is the create category page.</p>
<div>
    <input bind:value={title} type="text" placeholder="Category Name" />
    <button on:click={createCategory}>Create Category</button>
</div>
