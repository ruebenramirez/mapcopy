using System;

namespace CleanMapCopy
{
	public sealed class paintProgressDelegate : MulticastDelegate
	{
		public paintProgressDelegate(object @object, IntPtr method);

		public virtual IAsyncResult BeginInvoke(AsyncCallback callback, object @object);

		public virtual void EndInvoke(IAsyncResult result);

		public virtual void Invoke();
	}
}