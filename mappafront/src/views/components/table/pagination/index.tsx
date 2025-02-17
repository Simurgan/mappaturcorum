import arrowForward from "@/assets/icons/arrow-forward.svg";
import "./style.scss";

interface TablePaginationProps {
  currentPage: number;
  totalPage: number;
  nextStep?: (page: number) => void;
  prevStep?: (page: number) => void;
  goStep?: (page: number) => void;
}

const TablePagination = ({
  currentPage,
  totalPage,
  nextStep,
  prevStep,
  goStep,
}: TablePaginationProps) => {
  const generatePageNumbers = (): (number | string)[] => {
    const maxVisiblePages = 5;
    let pages: (number | string)[] = [];

    if (totalPage <= maxVisiblePages) {
      // Sayfa sayısı 5 veya daha azsa hepsini göster
      return Array.from({ length: totalPage }, (_, i) => i + 1);
    }

    if (currentPage <= 3) {
      // Kullanıcı ilk sayfalardaysa
      pages = [1, 2, 3, "...", totalPage];
    } else if (currentPage >= totalPage - 2) {
      // Kullanıcı son sayfalardaysa
      pages = [1, "...", totalPage - 2, totalPage - 1, totalPage];
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
        onClick={() => currentPage > 1 && prevStep!(currentPage - 1)}
        disabled={currentPage === 1}
      >
        <img src={arrowForward} alt="arrow" className="rotate-180" />
      </button>

      {pageNumbers.map((page, index) => (
        <button
          key={index}
          className={`page-item ${currentPage === page ? "active" : ""} ${
            typeof page !== "number" ? "dots" : ""
          }`}
          onClick={() => typeof page === "number" && goStep!(page)}
          disabled={typeof page !== "number"}
        >
          {page}
        </button>
      ))}

      <button
        className="next"
        onClick={() => currentPage < totalPage && nextStep!(currentPage + 1)}
        disabled={currentPage === totalPage}
      >
        <img src={arrowForward} alt="arrow" />
      </button>
    </nav>
  );
};

export default TablePagination;
