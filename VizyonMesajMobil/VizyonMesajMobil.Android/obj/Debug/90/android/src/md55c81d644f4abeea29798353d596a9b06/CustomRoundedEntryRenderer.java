package md55c81d644f4abeea29798353d596a9b06;


public class CustomRoundedEntryRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.EntryRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("VizyonMesajMobil.Droid.Custom_Renderers.CustomRoundedEntryRenderer, VizyonMesajMobil.Android", CustomRoundedEntryRenderer.class, __md_methods);
	}


	public CustomRoundedEntryRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomRoundedEntryRenderer.class)
			mono.android.TypeManager.Activate ("VizyonMesajMobil.Droid.Custom_Renderers.CustomRoundedEntryRenderer, VizyonMesajMobil.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CustomRoundedEntryRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomRoundedEntryRenderer.class)
			mono.android.TypeManager.Activate ("VizyonMesajMobil.Droid.Custom_Renderers.CustomRoundedEntryRenderer, VizyonMesajMobil.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomRoundedEntryRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomRoundedEntryRenderer.class)
			mono.android.TypeManager.Activate ("VizyonMesajMobil.Droid.Custom_Renderers.CustomRoundedEntryRenderer, VizyonMesajMobil.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
