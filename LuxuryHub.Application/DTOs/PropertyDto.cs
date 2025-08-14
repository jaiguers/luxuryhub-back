namespace LuxuryHub.Application.DTOs;

public class PropertyDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = string.Empty;
    public int Year { get; set; }
    public string IdOwner { get; set; } = string.Empty;
    public OwnerDto? Owner { get; set; }
    public string? MainImage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class OwnerDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
}

public class PropertyImageDto
{
    public string Id { get; set; } = string.Empty;
    public string IdProperty { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PropertyTraceDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime DateSale { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
    public string IdProperty { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}
