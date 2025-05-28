using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video v1 = new Video("Funny Cats", "AnimalLover", 120);
        v1.Comments.Add(new Comment("Anna", "So cute!"));
        v1.Comments.Add(new Comment("Ben", "I love this!"));
        v1.Comments.Add(new Comment("Cara", "Watched this 10 times!"));

        Video v2 = new Video("How to Draw", "ArtPro", 300);
        v2.Comments.Add(new Comment("Dan", "This helped a lot."));
        v2.Comments.Add(new Comment("Ella", "Great tutorial!"));
        v2.Comments.Add(new Comment("Finn", "I drew along with you."));

        Video v3 = new Video("Travel Vlog", "WorldExplorer", 480);
        v3.Comments.Add(new Comment("Grace", "Beautiful places."));
        v3.Comments.Add(new Comment("Henry", "Where is this?"));
        v3.Comments.Add(new Comment("Ivy", "Adding this to my bucket list."));

        // Store in a list
        List<Video> videos = new List<Video> { v1, v2, v3 };

        // Display all videos
        foreach (Video video in videos)
        {
            video.Display();
        }
    }
}
