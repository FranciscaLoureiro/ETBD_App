﻿namespace ETBD.Data.Entities;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
