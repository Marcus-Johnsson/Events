  <script lang="ts">
      import ApiService from "$lib/services/apiService";
      import { CategoryPostService } from "$lib/services/category/getCategory";
      import { onMount } from "svelte";
      import { updateCategory } from "$lib/services/category/updateCategory";
      import type { UpdateCategoriesData } from "$lib/services/category/putCategory";
      import { text } from "@sveltejs/kit";
      import DeleteButtonComponent from "$lib/components/DeteleButtonComponent.svelte";
      import {createCategory} from "$lib/services/category/createCategorybutton";

      let choosenCategory: number | null = $state(null);
      let title = $state("");
      let createTitle = $state("");
    
      const apiService = new ApiService();

      interface Category{
          title: string;
          id: number;
      }
      let categories = $state<Category[]>([]);

      onMount(async () => {
      categories = await new CategoryPostService(apiService).GetCategories();
      categories.sort((a: Category, b: Category) => b.id - a.id );
      });

      async function confirmOption(updatedCategory: UpdateCategoriesData) {
        updatedCategory= {
          id: choosenCategory!,
          title
        };
        updateCategory(updatedCategory);

        const idx = categories.findIndex(c => c.id === updatedCategory.id);
        if (idx !== -1) {
        categories[idx].title = updatedCategory.title; // direct mutation works with $state
        }

        choosenCategory = null; 
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
      <tr>
        <td>
          <input bind:value={createTitle} type="text">
        </td>
        <td>
          <button onclick={() => {createCategory(createTitle)}}>Create</button>
        </td>
      </tr>
      {#each categories as category}
        <tr style="background-color: {category.id % 2 === 0 ? 'rgb(95, 62, 62)' : 'rgb(124, 100, 56)'}">
          {#if choosenCategory !== category.id}
            <td data-id={category.id}>{category.title}</td>
            <td>
              <button onclick={() => {
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
          
              {#if choosenCategory === category.id}
                  <td><input type="text" bind:value={title}/> </td>
                {:else}
                  <td>{category.title}</td> 
              {/if}
            
          
            <td>
              <button onclick={() => {
                const updatedCategory: UpdateCategoriesData = {
                  id: category.id,
                  title
                };
                confirmOption(updatedCategory);
                choosenCategory = null;
              }}>  {'Save'}
              </button>
            </td>
            <td>
              <button onclick={() => {choosenCategory = null} }>Cancel</button>
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