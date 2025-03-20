// hooks/useDataFetcher.ts
import { useState, useEffect, useCallback } from "react";

/**
 * A general-purpose hook for data fetching.
 *
 * @param fetchFn   An async function that returns the data you want to fetch.
 * @param deps      A list of dependencies that, when changed, re-trigger `fetchFn`.
 *                  By default, it’s an empty array (fetch once).
 * @param options?  An options object which holds a boolean inside called disabled.
 *                  "Disabled" is a boolean which prevents the fetch operation if given and true.
 *
 * @returns         { data, loading, error, refetch }
 */
function useDataFetcher<T>(
  fetchFn: () => Promise<T>,
  deps: any[] = [],
  options: { disabled?: boolean } = {}
): [T | null, boolean, any, () => void] {
  const [data, setData] = useState<T | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<any>(null);

  const { disabled = false } = options; // default false if not provided

  // We wrap our fetch logic in useCallback so `refetch` stays stable.
  const fetchData = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);
      const response = await fetchFn();
      setData(response);
    } catch (err) {
      setError(err);
    } finally {
      setLoading(false);
    }
  }, [fetchFn]);

  // This function can be called to manually trigger a new fetch.
  const refetch = useCallback(() => {
    if (!disabled) fetchData();
  }, [disabled, fetchData]);

  // Trigger the fetch whenever the dependencies change.
  useEffect(() => {
    if (!disabled) fetchData();
  }, [...deps, disabled]);

  return [data, loading, error, refetch];
}

export default useDataFetcher;
