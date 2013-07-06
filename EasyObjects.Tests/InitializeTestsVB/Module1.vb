Imports EasyObjects.Tests.SQL
Imports NCI.EasyObjects

Module Module1

    Sub Main()

        Console.WriteLine("Refreshing database")
        UnitTestBase.RefreshDatabase()

        Dim obj As SqlComputedColumnFixture = New SqlComputedColumnFixture
        obj.Init()
        obj.Dynamic1Insert()

        Console.WriteLine("Press ENTER to continue...")
        Console.ReadLine()

    End Sub

End Module
