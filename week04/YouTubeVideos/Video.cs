using System;
using System.Collections.Generic;

public class Video
{
    private string _title;
    private string _author;
    private int _length;
    public List<Comment> Comments;

    public Video(string title, string author, int length)
    {
        _title = title;
        _author = author;
        _length = length;
        Comments = new List<Comment>();
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void Display()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_length} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (Comment c in Comments)
        {
            Console.WriteLine($"  - {c.Name}: {c.Text}");
        }
        Console.WriteLine();
    }
}