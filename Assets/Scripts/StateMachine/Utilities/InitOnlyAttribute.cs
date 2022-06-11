using System;
using UnityEngine;

namespace FSM
{
	[AttributeUsage(AttributeTargets.Field)]
	public class InitOnlyAttribute : PropertyAttribute { }
}
