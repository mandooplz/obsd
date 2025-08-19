using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MemoingTests")]
namespace MemoingCore;


// Object
public class Memoing
{
    // core
    public Memoing()
    {
        MemoingManager.Register(this);
    }
    public void Delete()
    {
        MemoingManager.Unregister(this.Id);
    }


    // state
    public readonly ID Id = new();

    internal HashSet<Memo.ID> Memos = new();


    // action
    public void CreateNewMemo()
    {
        // mutate
        var newMemoRef = new Memo();
        this.Memos.Add(newMemoRef.Id);
    }


    // value
    public record struct ID
    {
        public readonly Guid value;

        public ID()
        {
            this.value = Guid.NewGuid();
        }
    }
}


// ObjectManager
internal class MemoingManager
{
    internal static Dictionary<Memoing.ID, Memoing> container = new();
    internal static void Register(Memoing obj)
    {
        container.Add(obj.Id, obj);
    }
    internal static void Unregister(Memoing.ID id)
    {
        container.Remove(id);
    }
}