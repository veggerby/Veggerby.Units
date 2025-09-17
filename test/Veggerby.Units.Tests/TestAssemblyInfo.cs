using Xunit;

// Disables test parallelization because tests toggle global ReductionSettings feature flags.
// Without this, concurrent tests can observe mid-mutation states causing intermittent equality failures.
[assembly: CollectionBehavior(DisableTestParallelization = true)]