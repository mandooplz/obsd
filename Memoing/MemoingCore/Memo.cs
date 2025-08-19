using System;
using System.Collections.Generic;
using System.Text;

namespace MemoingCore;


// Object
public class Memo
{
    // core
    public Memo() {  }


    // state
    public readonly ID Id = new();

    public string title = "New Memo";
    public string body = "Enter the Memo's body";


    // action
    public void Remove()
    {
        Console.WriteLine("구현 예정입니다.");
    }


    // value
    public record struct ID
    {
        public readonly Guid value = Guid.NewGuid();

        public ID()
        {
            this.value = Guid.NewGuid();
        }
    }
}


// ObjectManager
