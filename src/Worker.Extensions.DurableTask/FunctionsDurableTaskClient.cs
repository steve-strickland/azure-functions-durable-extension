// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;

namespace Microsoft.Azure.Functions.Worker;

/// <summary>
/// A <b>per-binding</b> wrapper around an existing <see cref="DurableTaskClient" />. This is used to
/// hold onto some binding-specific details for functions-specific features and behavior.
/// </summary>
internal sealed class FunctionsDurableTaskClient : DurableTaskClient
{
    private readonly DurableTaskClient inner;

    public FunctionsDurableTaskClient(DurableTaskClient inner, string? queryString)
        : base(inner.Name)
    {
        this.inner = inner;
        this.QueryString = queryString;
    }

    public string? QueryString { get; }

    public override ValueTask DisposeAsync()
    {
        // We do not dispose inner client as it has a longer life than this class.
        return default;
    }

    public override AsyncPageable<OrchestrationMetadata> GetAllInstancesAsync(OrchestrationQuery? filter = null)
    {
        return this.inner.GetAllInstancesAsync(filter);
    }

    public override Task<OrchestrationMetadata?> GetInstancesAsync(
        string instanceId, bool getInputsAndOutputs = false, CancellationToken cancellation = default)
    {
        return this.inner.GetInstancesAsync(instanceId, getInputsAndOutputs, cancellation);
    }

    public override Task<PurgeResult> PurgeAllInstancesAsync(
        PurgeInstancesFilter filter, CancellationToken cancellation = default)
    {
        return this.inner.PurgeAllInstancesAsync(filter, cancellation);
    }

    public override Task<PurgeResult> PurgeInstanceAsync(
        string instanceId, CancellationToken cancellation = default)
    {
        return this.inner.PurgeInstanceAsync(instanceId, cancellation);
    }

    public override Task RaiseEventAsync(
        string instanceId, string eventName, object? eventPayload = null, CancellationToken cancellation = default)
    {
        return this.inner.RaiseEventAsync(instanceId, eventName, eventPayload, cancellation);
    }

    public override Task ResumeInstanceAsync(
        string instanceId, string? reason = null, CancellationToken cancellation = default)
    {
        return this.inner.ResumeInstanceAsync(instanceId, reason, cancellation);
    }

    public override Task<string> ScheduleNewOrchestrationInstanceAsync(
        TaskName orchestratorName,
        object? input = null,
        StartOrchestrationOptions? options = null,
        CancellationToken cancellation = default)
    {
        return this.inner.ScheduleNewOrchestrationInstanceAsync(orchestratorName, input, options, cancellation);
    }

    public override Task SuspendInstanceAsync(
        string instanceId, string? reason = null, CancellationToken cancellation = default)
    {
        return this.inner.SuspendInstanceAsync(instanceId, reason, cancellation);
    }

    public override Task TerminateInstanceAsync(
        string instanceId, object? output = null, CancellationToken cancellation = default)
    {
        return this.inner.TerminateInstanceAsync(instanceId, output, cancellation);
    }

    public override Task<OrchestrationMetadata> WaitForInstanceCompletionAsync(
        string instanceId, bool getInputsAndOutputs = false, CancellationToken cancellation = default)
    {
        return this.inner.WaitForInstanceCompletionAsync(instanceId, getInputsAndOutputs, cancellation);
    }

    public override Task<OrchestrationMetadata> WaitForInstanceStartAsync(
        string instanceId, bool getInputsAndOutputs = false, CancellationToken cancellation = default)
    {
        return this.inner.WaitForInstanceStartAsync(instanceId, getInputsAndOutputs, cancellation);
    }
}
