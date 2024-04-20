export interface PagedSearchResponse<T> {
  total: number;
  items: {
    $values: T[];
  };
}
