<script lang="ts">
    import ApiService from "$lib/services/apiService";
    import { CategoryPostService } from "$lib/services/category/GetCategory";
    import { onMount } from "svelte";
    import { updateCategory } from "$lib/services/category/updateCategory";
    import type { UpdateCategoriesData } from "$lib/services/category/PatchCategory";
    import { text } from "@sveltejs/kit";
    import {CategoryDeleteService} from "$lib/services/category/DeleteCategory";
    import DeleteButtonComponent from "$lib/components/deteleButtonComponent.svelte";

    let choosenCategory: number | null = null;
    let title = "";
    let deleteId : null | number = null;
    let selectedCategory = "";
   
    const apiService = new ApiService();
    interface Category{
        title: string;
        id: number;
    }
    let categories: Category[] = [];
    const categoryDeleteService = new CategoryDeleteService(apiService);

    onMount(async () => {
    categories = await new CategoryPostService(apiService).GetCategories();
    });
    async function confirmOption() {
    if (deleteId === null) {
      const updatedCategory: UpdateCategoriesData = {
        id: parseInt(selectedCategory),
        title
      };
      updateCategory(updatedCategory);
    } else {
     await categoryDeleteService.deleteCategory({id: deleteId});
    }
    choosenCategory = null; // Reset after updating
    }

    function handleDeleted(event: CustomEvent<{ id: number }>) {
        const deletedId = event.detail.id;
        categories = categories.filter(category => category.id !== deletedId);
    }
</script>

<table>
  <thead>
    <tr>
      <th>Categories</th>
    </tr>
  </thead>
  <tbody>
    {#each categories as category}
      <tr style="background-color: {category.id % 2 === 0 ? 'rgb(95, 62, 62)' : 'rgb(124, 100, 56)'}">
        {#if choosenCategory !== category.id && deleteId != category.id}
          <td>{category.title}</td>
          <td>
            <button on:click={() => {
              choosenCategory = category.id;
              title = category.title; 
            }}> Ändra</button>
          </td>
          <td>
            <DeleteButtonComponent
              resource="categories"
              id={category.id}
              on:deleted={handleDeleted}
            />
          </td>
        {:else}
          <td>
            <input type="text" bind:value={title}/>
          </td>
          <td>
            <button on:click={() => {
              const updatedCategory: UpdateCategoriesData = {
                id: category.id,
                title
              };
              updateCategory(updatedCategory);
              choosenCategory = null;
            }}>  {deleteId !== null ? 'Delete' : 'Save'}
            </button>
          </td>
          <td>
            <button on:click={() => {choosenCategory = null, deleteId = null} }>Cancel</button>
          </td>
        {/if}
      </tr>
    {/each}
  </tbody>
</table>


<style>
      th {
    width: 25%;
    background-color: rgb(126, 95, 41);
    color: rgb(24, 23, 23)!important;
    
  }
  table, th, td {
    border: 1px solid rgb(58, 55, 55);
    padding: 10px;
    max-width: 800px;
    
  }
  td {
    text-align: center;
    min-height: 40px;
    min-width: 10px;
  }
  tr {
    height: 40px;
    color: rgb(206, 202, 202);
  }
</style>