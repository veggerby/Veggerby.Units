#!/usr/bin/env bash
set -euo pipefail
baseline=$1
current=$2
allowed=${3:-1.0}

jq -r 'to_entries[] | [.key, (.value.MeanNs|tostring), (.value.AllocB|tostring)] | @tsv' "$baseline" > /tmp/base.tsv
jq -r 'to_entries[] | [.key, (.value.MeanNs|tostring), (.value.AllocB|tostring)] | @tsv' "$current" > /tmp/curr.tsv

fail=0
while IFS=$'\t' read -r name bMean bAlloc; do
  cLine=$(grep -P "^${name}\t" /tmp/curr.tsv || true)
  if [[ -z "$cLine" ]]; then
    echo "Missing benchmark '$name' in current results" >&2
    fail=1
    continue
  fi
  cMean=$(echo "$cLine" | cut -f2)
  cAlloc=$(echo "$cLine" | cut -f3)
  if [[ "$bMean" != 0 ]]; then
    regression=$(awk -v b=$bMean -v c=$cMean 'BEGIN{ printf "%.4f", ((c-b)/b)*100 }')
    gt=$(awk -v r=$regression -v a=$allowed 'BEGIN{ if (r>a) print 1; else print 0 }')
    if [[ $gt -eq 1 ]]; then
      echo "Mean regression ${regression}% > ${allowed}% for $name (baseline=${bMean}ns current=${cMean}ns)" >&2
      fail=1
    fi
  fi
  if [[ $cAlloc -gt $bAlloc ]]; then
    echo "Alloc increase for $name (baseline=${bAlloc}B current=${cAlloc}B)" >&2
    fail=1
  fi
done < /tmp/base.tsv

if [[ $fail -eq 1 ]]; then
  echo "PERFORMANCE REGRESSIONS" >&2
  exit 1
else
  echo "No performance regressions detected."
fi
