# Quantity Kind Tag Roots

Lightweight governance over semantic tags keeps flexibility while preventing accidental fragmentation (e.g. `Thermodynamic` vs `Thermodynamics`). Reserved roots are advisory; DEBUG builds emit warnings for non‑reserved roots unless explicitly whitelisted.

| Root | Intent / Domain | Example Tags | Notes |
|------|-----------------|--------------|-------|
| Energy | Thermodynamic & mechanical energy semantics | Energy.StateFunction, Energy.PathFunction | State vs path delineation lives under Energy.* |
| Domain | Broad physical discipline or application domain | Domain.Thermodynamic, Domain.Mechanical, Domain.Electromagnetic | Use singular discipline names |
| Form | Structural / mathematical form descriptors | Form.Dimensionless, Form.Vector | Avoid treating Form.* as granting scalar privileges |
| State | Point-like / absolute semantic qualifiers | State.Absolute, State.Reference | Reserved for potential future absolute kinds |
| Path | Path-dependent transfer semantics | Path.Transfer, Path.Dissipation | Distinguishes non-state cumulative processes |
| Geometry | Spatial / shape descriptors | Geometry.Angular, Geometry.Planar | For future spatial semantic tagging |
| Material | Material / substance oriented semantics | Material.Property, Material.Transport | Bindings to material science concepts |
| Process | Process phase / operation semantics | Process.Heating, Process.Cooling | Avoid overly granular runtime states |
| Signal | Information / signal processing semantics | Signal.FrequencyDomain | For frequency/time-domain classification |
| Temporal | Time semantics beyond raw duration | Temporal.Instant, Temporal.Interval | Potential future expansion (not yet used) |

Whitelist (intentionally outside reserved roots): `Legacy`, `Experimental`.

## Guidelines

1. Prefer an existing root; add new roots sparingly and only when multiple tags will share it.
2. Use singular nouns (`Domain.Thermodynamic`, not `Domains.Thermodynamics`).
3. Keep tags stable—rename only with clear migration guidance.
4. Do not overload tags for transient runtime states (stick to enduring semantics).
5. Avoid abbreviations unless overwhelmingly standard (e.g. `EM` avoided in favor of `Domain.Electromagnetic`).

## Soft Validation

In DEBUG builds the library scans built-in kinds once and emits warnings to `Trace` and stderr for:

- Unreserved, non-whitelisted roots.
- Plural roots (`...s`) that collide with a singular reserved root.

These are warnings only—no exceptions are thrown; production (Release) builds perform no checks.

## Manifest

At build packaging time (future extension) a JSON manifest can be emitted enumerating all tag names and reserved roots to assist external tooling in generating documentation diffs.

Example shape:

```json
{
  "roots": ["Energy", "Domain", "Form", "State", "Path"],
  "tags": ["Energy.StateFunction", "Energy.PathFunction", "Domain.Thermodynamic"]
}
```

## Extending

To introduce a new root:

- Add your tags referencing it (no code change required for runtime).
- Consider submitting a PR updating this table if the root is broadly useful.

If demand appears for analyzer enforcement or manifest emission, these will be added incrementally without breaking existing tags.
