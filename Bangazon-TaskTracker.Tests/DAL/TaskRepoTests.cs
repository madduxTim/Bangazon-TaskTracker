using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bangazon_TaskTracker.DAL;
using Moq;
using Bangazon_TaskTracker.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Bangazon_TaskTracker.Tests.DAL
{
    [TestClass]
    public class TaskRepoTests
    {
        private Mock<TaskContext> context = new Mock<TaskContext>();
        private Mock<DbSet<Task>> Tasks { get; set; }
        private List<Task> TaskList { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            Tasks = new Mock<DbSet<Task>>();
            TaskList = new List<Task>
            {
                new Task { Id = 1, Name = "Name field 1", Description = "Description field 1" },
                new Task { Id = 2, Name = "Name field 2", Description = "Description field 2" },
                new Task { Id = 3, Name = "Name field 3", Description = "Description field 3" },
                new Task { Id = 4, Name = "Name field 4", Description = "Description field 4" },
                new Task { Id = 5, Name = "Name field 5", Description = "Description field 5" }
            };
            context.Setup(x => x.Tasks).Returns(Tasks.Object);
            SetUpMocksAsQueryable();
        }

        public void SetUpMocksAsQueryable()
        {
            var taskQueryable = TaskList.AsQueryable();
            Tasks.As<IQueryable<Task>>().Setup(t => t.Provider).Returns(taskQueryable.Provider);
            Tasks.As<IQueryable<Task>>().Setup(t => t.Expression).Returns(taskQueryable.Expression);
            Tasks.As<IQueryable<Task>>().Setup(t => t.ElementType).Returns(taskQueryable.ElementType);
            Tasks.As<IQueryable<Task>>().Setup(t => t.GetEnumerator()).Returns(taskQueryable.GetEnumerator());
        }

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

        [TestMethod]
        public void CanReturnAllTasks()
        {
            //Arrange
            var repo = new TaskRepository(context.Object);
            //Act
            List<Task> actualTasks = repo.GetAll();
            //Assert
            Assert.IsNotNull(actualTasks);
            Assert.AreEqual(actualTasks.Count, 5);
        }
    }
}
