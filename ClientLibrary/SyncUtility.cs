using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ProjectOxford.EventKnowledge
{
    class SyncUtility
    {
        // TODO: When we move to .NET 4.5, we may be able to get rid of this method, or at least reduce our reliance upon it.
        // The ideal solution is to use async either everywhere or nowhere throughout a call to the Client SDK, but this may
        // not be possible (ProjectOxford.Common only exposes async APIs, and doesn't use ConfigureAwait(false), for example).
        // Blog post discussing this is here: https://blogs.msdn.com/b/pfxteam/archive/2012/04/13/10293638.aspx
        internal static void RunWithoutSynchronizationContext(Action actionToRun)
        {
            SynchronizationContext oldContext = SynchronizationContext.Current;
            try
            {
                SynchronizationContext.SetSynchronizationContext(null);
                actionToRun();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(oldContext);
            }
        }

        internal static T RunWithoutSynchronizationContext<T>(Func<T> actionToRun)
        {
            SynchronizationContext oldContext = SynchronizationContext.Current;
            try
            {
                SynchronizationContext.SetSynchronizationContext(null);
                return actionToRun();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(oldContext);
            }
        }
    }
}
