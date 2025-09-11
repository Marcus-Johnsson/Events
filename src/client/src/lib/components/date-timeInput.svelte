<script>
    let hours = Array.from({ length: 24 }, (_, i) => String(i).padStart(2, '0'));
    let minutes = ["00", "15", "30", "45"];

    let selectedHourStart = "00";
    let selectedMinuteStart = "00";

    let selectedHourEnd = "00";
    let selectedMinuteEnd = "00";

    let dateStart = "";
    let dateEnd = "";

    $: timeStart = `${selectedHourStart}:${selectedMinuteStart}`;
    $: timeEnd = `${selectedHourEnd}:${selectedMinuteEnd}`;

    $: startDateTime = new Date(`${dateStart}T${selectedHourStart}:${selectedMinuteStart}:00`);
    $: endDateTime = new Date(`${dateEnd}T${selectedHourEnd}:${selectedMinuteEnd}:00`);

    $: isEndBeforeStart = endDateTime < startDateTime;


</script>

<div>
    <input type="date" bind:value={dateStart}>
    <select bind:value={selectedHourStart}>
        {#each hours as h}
        <option value={h}>{h}</option>
        {/each}
  </select>
  :
    <select bind:value={selectedMinuteStart}>
        {#each minutes as m}
        <option value={m}>{m}</option>
        {/each}
    </select>
</div>
<p>Selected date : {dateStart} - {timeStart}</p>

<div>
        <input type="date" bind:value={dateEnd}>

    <select bind:value={selectedHourEnd}>
        {#each hours as h}
        <option value={h}>{h}</option>
        {/each}
    </select>
  :
    <select bind:value={selectedMinuteEnd}>
        {#each minutes as m}
        <option value={m}>{m}</option>
        {/each}
    </select>
</div>
<p>Selected date : {dateEnd} - {timeEnd}</p>

{#if isEndBeforeStart}
<p style="color: red;">Event can't end before start date!</p>
{/if}
