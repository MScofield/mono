//
// System.ComponentModel.UInt16Converter test cases
//
// Authors:
// 	Gert Driesen (drieseng@users.sourceforge.net)
//
// (c) 2005 Novell, Inc. (http://www.ximian.com)
//

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

using NUnit.Framework;

namespace MonoTests.System.ComponentModel
{
	[TestFixture]
	public class UInt16ConverterTests
	{
		private UInt16Converter converter;
		
		[SetUp]
		public void SetUp ()
		{
			converter = new UInt16Converter ();
		}

		[Test]
		public void CanConvertFrom ()
		{
			Assert.IsTrue (converter.CanConvertFrom (typeof (string)), "#1");
			Assert.IsFalse (converter.CanConvertFrom (typeof (short)), "#2");
			Assert.IsFalse (converter.CanConvertFrom (typeof (object)), "#3");
			Assert.IsTrue (converter.CanConvertFrom (typeof (InstanceDescriptor)), "#4");
		}

		[Test]
		public void CanConvertTo ()
		{
			Assert.IsTrue (converter.CanConvertTo (typeof (string)), "#1");
			Assert.IsFalse (converter.CanConvertTo (typeof (object)), "#2");
			Assert.IsTrue (converter.CanConvertTo (typeof (int)), "#3");
		}

		[Test]
		public void ConvertFrom_MinValue ()
		{
			Assert.AreEqual (ushort.MinValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "#0"), "#1");
			Assert.AreEqual (ushort.MinValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "#0x0"), "#2");
			Assert.AreEqual (ushort.MinValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "#0X0"), "#3");
			Assert.AreEqual (ushort.MinValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "0x0"), "#4");
			Assert.AreEqual (ushort.MinValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "0X0"), "#5");
		}

		[Test]
		public void ConvertFrom_MaxValue ()
		{
			Assert.AreEqual (ushort.MaxValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "#ffff"), "#1");
			Assert.AreEqual (ushort.MaxValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "#FFFF"), "#2");
			Assert.AreEqual (ushort.MaxValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "#0xffff"), "#3");
			Assert.AreEqual (ushort.MaxValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "#0XFFFF"), "#4");
			Assert.AreEqual (ushort.MaxValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "0xffff"), "#5");
			Assert.AreEqual (ushort.MaxValue, converter.ConvertFrom (null, CultureInfo.InvariantCulture, "0XFFFF"), "#6");
		}

		[Test]
		[ExpectedException (typeof (NotSupportedException))]
		public void ConvertFrom_Object ()
		{
			converter.ConvertFrom (new object ());
		}

		[Test]
		[ExpectedException (typeof (NotSupportedException))]
		public void ConvertFrom_Int16 ()
		{
			converter.ConvertFrom ((short) 10);
		}

		[Test]
		[ExpectedException (typeof (NotSupportedException))]
		public void ConvertFrom_Int32 ()
		{
			converter.ConvertFrom (10);
		}

		[Test]
		public void ConvertTo_MinValue ()
		{
			Assert.AreEqual (ushort.MinValue.ToString (CultureInfo.InvariantCulture),
				converter.ConvertTo (null, CultureInfo.InvariantCulture, ushort.MinValue,
				typeof (string)), "#1");
			Assert.AreEqual (ushort.MinValue.ToString (CultureInfo.CurrentCulture),
				converter.ConvertTo (null, CultureInfo.CurrentCulture, ushort.MinValue,
				typeof (string)), "#2");
			Assert.AreEqual (ushort.MinValue.ToString (CultureInfo.CurrentCulture),
				converter.ConvertTo (ushort.MinValue, typeof (string)), "#3");
		}

		[Test]
		public void ConvertTo_MaxValue ()
		{
			Assert.AreEqual (ushort.MaxValue.ToString (CultureInfo.InvariantCulture),
				converter.ConvertTo (null, CultureInfo.InvariantCulture, ushort.MaxValue,
				typeof (string)), "#1");
			Assert.AreEqual (ushort.MaxValue.ToString (CultureInfo.CurrentCulture),
				converter.ConvertTo (null, CultureInfo.CurrentCulture, ushort.MaxValue,
				typeof (string)), "#2");
			Assert.AreEqual (ushort.MaxValue.ToString (CultureInfo.CurrentCulture),
				converter.ConvertTo (ushort.MaxValue, typeof (string)), "#3");
		}

		[Test]
		public void ConvertToString ()
		{
			CultureInfo culture = new MyCultureInfo ();
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo) culture.GetFormat (typeof (NumberFormatInfo));

			Assert.AreEqual (culture.NumberFormat.NegativeSign + "5", converter.ConvertToString (null, culture, -5));
		}

		[Test]
		public void ConvertFromString_Invalid1 ()
		{
			try {
				converter.ConvertFromString (null, CultureInfo.InvariantCulture, "*1");
				Assert.Fail ("#1");
			} catch (Exception ex) {
				Assert.AreEqual (typeof (Exception), ex.GetType (), "#2");
				Assert.IsNotNull (ex.InnerException, "#3");
				Assert.AreEqual (typeof (FormatException), ex.InnerException.GetType (), "#3");
			}
		}

		[Test]
		public void ConvertFromString_Invalid2 ()
		{
			try {
				converter.ConvertFromString (null, CultureInfo.InvariantCulture,
					double.MaxValue.ToString(CultureInfo.InvariantCulture));
				Assert.Fail ("#1");
			} catch (Exception ex) {
				Assert.AreEqual (typeof (Exception), ex.GetType (), "#2");
				Assert.IsNotNull (ex.InnerException, "#3");
				Assert.AreEqual (typeof (FormatException), ex.InnerException.GetType (), "#3");
			}
		}

		[Test]
		public void ConvertFromString_Invalid3 ()
		{
			try {
				converter.ConvertFromString ("*1");
				Assert.Fail ("#1");
			} catch (Exception ex) {
				Assert.AreEqual (typeof (Exception), ex.GetType (), "#2");
				Assert.IsNotNull (ex.InnerException, "#3");
				Assert.AreEqual (typeof (FormatException), ex.InnerException.GetType (), "#3");
			}
		}

		[Test]
		public void ConvertFromString_Invalid4 ()
		{
			try {
				converter.ConvertFromString (double.MaxValue.ToString (CultureInfo.CurrentCulture));
				Assert.Fail ("#1");
			} catch (Exception ex) {
				Assert.AreEqual (typeof (Exception), ex.GetType (), "#2");
				Assert.IsNotNull (ex.InnerException, "#3");
				Assert.AreEqual (typeof (FormatException), ex.InnerException.GetType (), "#3");
			}
		}

		[Test]
		public void ConvertFrom_InvalidString1 ()
		{
			try {
				converter.ConvertFrom (null, CultureInfo.InvariantCulture, "*1");
				Assert.Fail ("#1");
			} catch (Exception ex) {
				Assert.AreEqual (typeof (Exception), ex.GetType (), "#2");
				Assert.IsNotNull (ex.InnerException, "#3");
				Assert.AreEqual (typeof (FormatException), ex.InnerException.GetType (), "#3");
			}
		}

		[Test]
		public void ConvertFrom_InvalidString2 ()
		{
			try {
				converter.ConvertFrom (null, CultureInfo.InvariantCulture,
					double.MaxValue.ToString (CultureInfo.InvariantCulture));
				Assert.Fail ("#1");
			} catch (Exception ex) {
				Assert.AreEqual (typeof (Exception), ex.GetType (), "#2");
				Assert.IsNotNull (ex.InnerException, "#3");
				Assert.AreEqual (typeof (FormatException), ex.InnerException.GetType (), "#3");
			}
		}

		[Test]
		public void ConvertFrom_InvalidString3 ()
		{
			try {
				converter.ConvertFrom ("*1");
				Assert.Fail ("#1");
			} catch (Exception ex) {
				Assert.AreEqual (typeof (Exception), ex.GetType (), "#2");
				Assert.IsNotNull (ex.InnerException, "#3");
				Assert.AreEqual (typeof (FormatException), ex.InnerException.GetType (), "#3");
			}
		}

		[Test]
		public void ConvertFrom_InvalidString4 ()
		{
			try {
				converter.ConvertFrom (double.MaxValue.ToString (CultureInfo.CurrentCulture));
				Assert.Fail ("#1");
			} catch (Exception ex) {
				Assert.AreEqual (typeof (Exception), ex.GetType (), "#2");
				Assert.IsNotNull (ex.InnerException, "#3");
				Assert.AreEqual (typeof (FormatException), ex.InnerException.GetType (), "#3");
			}
		}

		[Serializable]
		private sealed class MyCultureInfo : CultureInfo
		{
			internal MyCultureInfo ()
				: base ("en-US")
			{
			}

			public override object GetFormat (Type formatType)
			{
				if (formatType == typeof (NumberFormatInfo)) {
					NumberFormatInfo nfi = (NumberFormatInfo) ((NumberFormatInfo) base.GetFormat (formatType)).Clone ();

					nfi.NegativeSign = "myNegativeSign";
					return NumberFormatInfo.ReadOnly (nfi);
				} else {
					return base.GetFormat (formatType);
				}
			}
// adding this override in 1.x shows different result in .NET (it is ignored).
// Some compatibility kids might want to fix this issue.
			public override NumberFormatInfo NumberFormat {
				get {
					NumberFormatInfo nfi = (NumberFormatInfo) base.NumberFormat.Clone ();
					nfi.NegativeSign = "myNegativeSign";
					return nfi;
				}
				set { throw new NotSupportedException (); }
			}
		}
	}
}
