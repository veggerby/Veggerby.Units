namespace Veggerby.Units.Quantities;

internal static partial class QuantityKindsInitializer
{
    static QuantityKindsInitializer()
    {
        // Collect all currently declared built-in kinds for soft validation (DEBUG only).
        var kinds = new System.Collections.Generic.List<QuantityKind>();
        foreach (var field in typeof(QuantityKinds).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
        {
            if (field.FieldType == typeof(QuantityKind) && field.GetValue(null) is QuantityKind k)
            {
                kinds.Add(k);
            }
        }

        QuantityKindTagExtensions.ValidateReservedRootsOnce(kinds);
    }
}