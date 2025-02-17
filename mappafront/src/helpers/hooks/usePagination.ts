import { useState, useMemo } from "react";

interface UsePaginationProps {
  _data: Object[];
  itemsPerPage?: number;
  initialPage?: number;
}

interface UsePaginationReturn {
  currentPage: number;
  totalPages: number;
  paginatedData: Object[];
  nextPage: () => void;
  prevPage: () => void;
  goToPage: (page: number) => void;
}

const usePagination = ({
  _data,
  itemsPerPage = 4, // Varsayılan olarak 4 öğe gösterilecek
  initialPage = 1,
}: UsePaginationProps): UsePaginationReturn => {
  const totalItems = _data.length;
  const totalPages = totalItems > 0 ? Math.ceil(totalItems / itemsPerPage) : 1;
  const [currentPage, setCurrentPage] = useState(
    Math.min(initialPage, totalPages)
  );

  // Sayfalara göre veriyi hesapla (useMemo ile optimize edildi)
  const paginatedData = useMemo(() => {
    const startIndex = (currentPage - 1) * itemsPerPage;
    return _data.slice(startIndex, startIndex + itemsPerPage);
  }, [_data, currentPage, itemsPerPage]);

  const nextPage = () => {
    setCurrentPage((prev) => (prev < totalPages ? prev + 1 : prev));
  };

  const prevPage = () => {
    setCurrentPage((prev) => (prev > 1 ? prev - 1 : prev));
  };

  const goToPage = (page: number) => {
    if (page >= 1 && page <= totalPages) {
      setCurrentPage(page);
    }
  };

  return {
    currentPage,
    totalPages,
    paginatedData, // Burada her sayfa için uygun verileri döndürür
    nextPage,
    prevPage,
    goToPage,
  };
};

export default usePagination;
