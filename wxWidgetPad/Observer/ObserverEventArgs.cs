using System;
using System.Collections.Generic;
using System.Text;

namespace wxWidgetPad.Observer
{
    public class ObserverEventArgs
    {
        public Queue<int> ChangedControls { get; set; }

        public ObserverEventArgs()
            : this(new Queue<int>(0))
        {
        }

        public ObserverEventArgs(Queue<int> changedControls)
        {
            this.ChangedControls = changedControls;
        }
    }
}