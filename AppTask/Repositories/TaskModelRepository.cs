﻿using AppTask.Database;
using AppTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTask.Repositories
{
    internal class TaskModelRepository : ITaskModelRepository
    {
        private AppTaskContext _db;
        public TaskModelRepository()
        {
            _db = new AppTaskContext();
        }

        public IList<TaskModel> GetAll()
        {
            return _db.Tasks.ToList();
        }

        public TaskModel GetById(int id)
        {
            return _db.Tasks.Include(a => a.SubTasks).FirstOrDefault(a => a.Id == id);
        }

        public void Add(TaskModel task)
        {
            _db.Tasks.Add(task);
            _db.SaveChanges();
        }
        public void Update(TaskModel task)
        {
            _db.Tasks.Update(task);
            _db.SaveChanges();
        }

        public void Delete(TaskModel task)
        {
            _db.Remove(task);
            _db.SaveChanges();
        }
    }
}