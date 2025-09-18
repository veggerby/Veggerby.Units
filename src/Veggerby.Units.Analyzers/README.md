# Veggerby.Units.Analyzers (MVP)

Diagnostics:

* VUNITS001: Incompatible unit addition/subtraction (MVP heuristic; unit symbol extraction TBD).
* VUNITS002: Ambiguous unit formatting without explicit UnitFormat (currently broad; future refinement to only ambiguous symbols).

Next steps: implement precise unit extraction and ambiguity registry consumption via shared source or public hook.
