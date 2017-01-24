using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bangazon_TaskTracker.DAL;
using Moq;

namespace Bangazon_TaskTracker.Tests.DAL
{
    [TestClass]
    public class TaskRepoTests
    {
        private Mock<TaskContext> context = new Mock<TaskContext>();
        [TestMethod]
        public void RepoHasContext()
        {
            TaskRepository repo = new TaskRepository();
            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void RepoHasAccessToPassedInContext()
        {
            TaskRepository repo = new TaskRepository(context.Object);
            Assert.IsNotNull(repo.Context);
            Assert.AreEqual(context.Object, repo.Context);
        }
    }
}
