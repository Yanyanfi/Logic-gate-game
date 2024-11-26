using System;
using System.Collections.Generic;
using System.ComponentModel;

public class ObservableList<T> : List<T> 
{
    public event Action ListChangedEventArgs;
    public new void Add(T item)
    {
        base.Add(item);

    }
}
