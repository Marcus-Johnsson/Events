namespace Events.Api.Categories;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class CreateCategoryRequest
{
    public string Title { get; set; } = string.Empty;
}

public class UpdateCategoryRequest
{
    public string Title { get; set; } = string.Empty;
}
