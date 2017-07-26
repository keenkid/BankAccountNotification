using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.TaskScheduler;

namespace SunGard.AvantGard.TaskSchedulerControl
{
    class TaskControlManager : ITaskControlManager, IDisposable
    {
        private Task _targetTask = null;

        private TaskService _taskService = null;

        public TaskControlManager(string bankCode)
        {
            var taskName = (from ControlElement control in TaskControlSection.Instance.TaskControls
                            where control.BankCode == bankCode
                            select control.TaskName).FirstOrDefault();
            InitializeTask(taskName);
        }

        public TaskControlManager(ControlElement element)
        {
            var taskName = element.TaskName;
            InitializeTask(taskName);
        }

        private void InitializeTask(string taskName)
        {
            if (null == _taskService)
            {
                _taskService = new TaskService();
            }

            try
            {
                _targetTask = _taskService.FindTask(taskName);
            }
            catch
            {
                //
            }
        }

        public void StartTask()
        {
            throw new NotImplementedException();
        }

        public void StopTask()
        {
            throw new NotImplementedException();
        }

        public void EnableTask()
        {
            if (_targetTask.State == TaskState.Disabled)
            {
                _targetTask.Enabled = true;
            }
        }

        public void DisableTask()
        {
            if (_targetTask.State == TaskState.Running)
            {
                _targetTask.Stop();
            }
            _targetTask.Enabled = false;
        }

        public void Dispose()
        {
            _taskService.Dispose();
        }
    }
}
