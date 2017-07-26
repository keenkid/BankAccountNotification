using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.TaskSchedulerControl
{
    interface ITaskControlManager : IDisposable
    {
        void StartTask();

        void StopTask();

        void EnableTask();

        void DisableTask();
    }
}
