using System;
using System.Collections.Generic;
using System.Text;

using MemoingCore;
namespace MemoingTests;

public class MemoingTests
{
    [Fact]
    public void Memoing_Creation_Should_Register_Object()
    {
        // given
        var memoingRef = new Memoing();

        Assert.NotNull(memoingRef);
        Assert.Empty(memoingRef.Memos);

        // when
        memoingRef.CreateNewMemo();

        // then
        Assert.Equal(memoingRef.Memos.Count, 1);
    }
}
