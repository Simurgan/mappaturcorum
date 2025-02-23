import arrowForward from "@/assets/icons/arrow-forward.svg";
import "./style.scss";

export type TablePaginationProps = {
  currentPage: number;
  totalPage: number;
  setPage: (page: number) => void;
};

const TablePagination = ({
  currentPage,
  totalPage,
  setPage,
}: TablePaginationProps) => {
  const generatePageNumbers = (): (number | string)[] => {
    const maxVisiblePages = 5;
    let pages: (number | string)[] = [];

    if (totalPage <= maxVisiblePages) {
      // Sayfa sayısı 5 veya daha azsa hepsini göster
      return Array.from({ length: totalPage }, (_, i) => i + 1);
    }

    if (currentPage < 4) {
      // Kullanıcı ilk sayfalardaysa
      pages = [1, 2, 3, 4, "...", totalPage];
    } else if (currentPage > totalPage - 3) {
      // Kullanıcı son sayfalardaysa
      pages = [
        1,
        "...",
        totalPage - 3,
        totalPage - 2,
        totalPage - 1,
        totalPage,
      ];
    } else {
      // Kullanıcı ortadaysa
      pages = [
        1,
        "...",
        currentPage - 1,
        currentPage,
        currentPage + 1,
        "...",
        totalPage,
      ];
    }

    return pages;
  };

  const pageNumbers: (number | string)[] = generatePageNumbers();

  return (
    <nav className="pagination">
      <button
        className="prev"
        onClick={() => {
          if (currentPage > 1) setPage(currentPage - 1);
        }}
        disabled={currentPage === 1}
      >
        <img src={arrowForward} alt="arrow" className="rotate-180" />
      </button>

      {pageNumbers.map((page, index) => (
        <button
          key={index}
          className={`page-item${currentPage === page ? " active" : ""}${
            typeof page !== "number" ? " dots" : ""
          }`}
          onClick={() => {
            if (typeof page === "number") setPage(page);
          }}
          disabled={page === "..." || page === currentPage ? true : false}
        >
          {page}
        </button>
      ))}

      <button
        className="next"
        onClick={() => {
          if (currentPage < totalPage) setPage(currentPage + 1);
        }}
        disabled={currentPage === totalPage}
      >
        <img src={arrowForward} alt="arrow" />
      </button>
    </nav>
  );
};

export default TablePagination;
