using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bangazon_TaskTracker.DAL;
using Moq;
using Bangazon_TaskTracker.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static Bangazon_TaskTracker.Models.Task;

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
                new Task { Id = 1, Name = "Name field 1", Description = "Description field 1", Status = eStatus.ToDo },
                new Task { Id = 2, Name = "Name field 2", Description = "Description field 2", Status = eStatus.ToDo },
                new Task { Id = 3, Name = "Name field 3", Description = "Description field 3", Status = eStatus.InProgress },
                new Task { Id = 4, Name = "Name field 4", Description = "Description field 4", Status = eStatus.Complete },
                new Task { Id = 5, Name = "Name field 5", Description = "Description field 5", Status = eStatus.Complete }
            };
            SetUpMocksAsQueryable();
        }

        public void SetUpMocksAsQueryable()
        {
            var taskQueryable = TaskList.AsQueryable();
            Tasks.As<IQueryable<Task>>().Setup(t => t.Provider).Returns(taskQueryable.Provider);
            Tasks.As<IQueryable<Task>>().Setup(t => t.Expression).Returns(taskQueryable.Expression);
            Tasks.As<IQueryable<Task>>().Setup(t => t.ElementType).Returns(taskQueryable.ElementType);
            Tasks.As<IQueryable<Task>>().Setup(t => t.GetEnumerator()).Returns(taskQueryable.GetEnumerator());
            context.Setup(x => x.Tasks).Returns(Tasks.Object);
            Tasks.Setup(t => t.Add(It.IsAny<Task>())).Callback((Task t) => TaskList.Add(t));
            Tasks.Setup(t => t.Remove(It.IsAny<Task>())).Callback((Task t) => TaskList.Remove(t));
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

        [TestMethod]
        public void CanAddTask()
        {
            //Arrange
            TaskRepository repo = new TaskRepository(context.Object);
            Task newTask = new Task { Id = 6, Name = "Name field 6", Description = "Description field 6" };
            //Act                        
            repo.AddTask(newTask);
            //List<Task> taskList = repo.GetAll(); 
            //Assert
            Assert.AreEqual(repo.Context.Tasks.Count(), 6);
        }

        [TestMethod]
        public void CanDeleteTask()
        {
            //Arrange
            TaskRepository repo = new TaskRepository(context.Object);
            Task targetTask = new Task { Id = 6, Name = "Name field 6", Description = "Description field 6" };
            int targetTaskId = targetTask.Id;
            //Act
            repo.AddTask(targetTask);
            //List<Task> taskList = repo.GetAll(); // I need some assistance REALLY getting this... and line 83
            //Assert
            Assert.AreEqual(repo.Context.Tasks.Count(), 6);
            //Act Again 
            repo.RemoveTask(targetTaskId);
            //Assert Again 
            Assert.AreEqual(repo.Context.Tasks.Count(), 5);
        }

        [TestMethod]
        public void CanUpdateTask()
        {
            //Arrange
            var repo = new TaskRepository(context.Object);
            Task targetTask = new Task { Id = 6, Name = "Old", Description = "oldDescription", CompletedOn = new DateTime(2009, 8, 1, 0, 0, 0), Status = eStatus.InProgress };
            //Act
            repo.AddTask(targetTask);
            //Thread.Sleep(5000);
            Task update = new Task { Id = 6, Name = "New", Description = "newDescription", CompletedOn = DateTime.Now, Status = eStatus.Complete };
            repo.UpdateTask(update);
            //Assert             
            Assert.AreEqual(repo.Context.Tasks.First(t => t.Id == 6).Name, "New");
            Assert.AreEqual(repo.Context.Tasks.First(t => t.Id == 6).Description, "newDescription");
            Assert.AreEqual(repo.Context.Tasks.First(t => t.Id == 6).CompletedOn, targetTask.CompletedOn);
            Assert.AreEqual(repo.Context.Tasks.First(t => t.Id == 6).Status, eStatus.Complete);
            Assert.AreEqual(repo.Context.Tasks.Count(), 6);
        }

        [TestMethod]
        public void WillReturnNullIfIdNotFoundForUpdate()
        {
            //Arrange
            var repo = new TaskRepository(context.Object);
            //Act
            Task errorTask = new Task { Id = 1000, Name = "Name", Description = "Description" };
            var updateResult = repo.UpdateTask(errorTask);
            //Assert
            Assert.IsNull(updateResult);
        }

        [TestMethod]
        public void CanReturnAllOfAnyParticularStatus()
        {
            //Arrange
            var repo = new TaskRepository(context.Object);
            //Act
            int status1 = 1;
            int status2 = 2;
            int status3 = 3;
            List<Task> todoTasks = repo.GetTasksByStatus(status1);
            List<Task> inProgressTasks = repo.GetTasksByStatus(status2);
            List<Task> doneTasks = repo.GetTasksByStatus(status3);
            //Assert
            Assert.AreEqual(todoTasks.Count, 2);
            Assert.AreEqual(inProgressTasks.Count, 1);
            Assert.AreEqual(doneTasks.Count, 2);
        }
    }
}
