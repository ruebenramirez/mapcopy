using System;

namespace CleanMapCopy
{
	public sealed class updateStateExtractingDelagate : MulticastDelegate
	{
		public updateStateExtractingDelagate(object @object, IntPtr method);

		public virtual IAsyncResult BeginInvoke(string state, AsyncCallback callback, object @object);

		public virtual void EndInvoke(IAsyncResult result);

		public virtual void Invoke(string state);
	}
}