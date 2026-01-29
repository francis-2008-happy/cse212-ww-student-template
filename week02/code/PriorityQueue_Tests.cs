using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several items with different priorities and dequeue to verify highest priority is removed first
    // Expected Result: Items should be dequeued in order of priority (highest first): "High" (5), "Medium" (3), "Low" (1)
    // Defect(s) Found: 
    // 1. Loop condition used _queue.Count - 1 which skips the last element ("High" at index 2).
    // 2. Item was never removed from queue after finding highest priority.
    public void TestPriorityQueue_DequeueSingleHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Enqueue several items where multiple have the same highest priority, verify FIFO order for same priority
    // Expected Result: When two items have the same highest priority (5), the first one enqueued ("First") should be dequeued first
    // Defect(s) Found: None for this specific test - it passed because "First" was at index 0 which is checked first.
    // However, the code has a bug with >= instead of > that could cause issues with different orderings.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Second", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("First", result);
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: None - Test passed. Exception is correctly thrown with the correct message.
    public void TestPriorityQueue_EmptyQueueException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue and dequeue multiple items to verify queue correctly removes highest priority each time
    // Expected Result: Items removed in order: "C" (10), "A" (5), "B" (3), "D" (1)
    // Defect(s) Found: 
    // 1. Loop condition used _queue.Count - 1 which skips the last element.
    // 2. Item was never removed from queue - same item kept being returned.
    public void TestPriorityQueue_MultipleDequeues()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 10);
        priorityQueue.Enqueue("D", 1);

        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test that the last item in the queue can be dequeued correctly (edge case for loop boundary)
    // Expected Result: When items have priorities where highest is at the end, it should still be found and removed
    // Defect(s) Found: Loop condition _queue.Count - 1 caused the last element (index 2, "Third" with priority 10) to be skipped.
    public void TestPriorityQueue_HighestPriorityAtEnd()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Third", 10);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Third", result);
    }
}