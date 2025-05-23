using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private static Random _rand = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        foreach (var word in text.Split(' '))
            _words.Add(new Word(word));
    }

    public void Display()
    {
        Console.WriteLine(_reference);
        foreach (var word in _words)
            Console.Write(word + " ");
        Console.WriteLine();
    }

    public void HideRandomWords(int count = 3)
    {
        var visibleIndices = new List<int>();
        for (int i = 0; i < _words.Count; i++)
            if (!_words[i].IsHidden())
                visibleIndices.Add(i);

        int toHide = Math.Min(count, visibleIndices.Count);
        for (int i = 0; i < toHide; i++)
        {
            int idx = _rand.Next(visibleIndices.Count);
            _words[visibleIndices[idx]].Hide();
            visibleIndices.RemoveAt(idx);
        }
    }

    public bool AllWordsHidden()
    {
        foreach (var word in _words)
            if (!word.IsHidden())
                return false;
        return true;
    }
}