namespace Library.Business.Dto;

public abstract class BaseBookDto
{
    public string? Isbn { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public string? Description { get; set; }
}