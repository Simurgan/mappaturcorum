import { getOrdinary } from "@/actions/ordinary-people";
import useDataFetcher from "@/hooks/data-fetcher";
import { SubObjectPair } from "@/models/ordinary-people";
import Button from "@/views/components/button";
import Text from "@/views/components/text";
import ReactModal from "react-modal";
import { ClipLoader } from "react-spinners";

interface OrdinaryPersonModalProps {
  ordinaryId?: number;
  isOpen: boolean;
  closeModal: () => void;
}

const OrdinaryPersonModal = ({
  ordinaryId,
  isOpen,
  closeModal,
}: OrdinaryPersonModalProps) => {
  const [ordinaryPersonResponse, isLoading, isError] = useDataFetcher(
    () => getOrdinary(ordinaryId as number),
    [ordinaryId],
    { disabled: ordinaryId === undefined || ordinaryId === null }
  );

  return (
    <ReactModal
      isOpen={isOpen}
      contentLabel="Example Modal"
      overlayClassName="custom-overlay"
      className="custom-mappa-modal"
    >
      <div className="modal-content">
        <Button onClick={closeModal} classNames="modal-close-button">
          <img src={require("@/assets/icons/close-icon.svg")} />
        </Button>

        <div className="content">
          {isError ? (
            <Text>Sorry, something went wrong!</Text>
          ) : isLoading ? (
            <ClipLoader />
          ) : (
            ordinaryPersonResponse && (
              <>
                <Text fs={28} fw={700} lh={140} color="burgundy">
                  {ordinaryPersonResponse.data.name || "No Name Provided"}
                </Text>

                <div className="content-info">
                  {ordinaryPersonResponse.data.alternateName && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Alternate Name(s):
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.alternateName}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.ethnicity && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Ethnicity:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.ethnicity.name}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.religion && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Religion:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.religion.name}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.formerReligion && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Former Religion:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.formerReligion.name}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.profession && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Profession:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.profession.name}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.professionExplanation && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Explanation of Profession:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.professionExplanation}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.gender && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Gender:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.gender.name}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.interestingFeature && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Additional Notes:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.interestingFeature}
                        </Text>
                      </div>
                    </>
                  )}

                  {((ordinaryPersonResponse.data.interactionsWithOrdinaryA &&
                    ordinaryPersonResponse.data.interactionsWithOrdinaryA
                      .length > 0) ||
                    (ordinaryPersonResponse.data.interactionsWithOrdinaryB &&
                      ordinaryPersonResponse.data.interactionsWithOrdinaryB
                        .length > 0)) && (
                    <div className="info-row">
                      <>
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Interactions With Ordinary People:
                        </Text>
                        {() => {
                          let ordinaryInteractions: SubObjectPair[] = [];
                          if (
                            ordinaryPersonResponse.data
                              .interactionsWithOrdinaryA
                          ) {
                            ordinaryInteractions = [
                              ...ordinaryInteractions,
                              ...ordinaryPersonResponse.data
                                .interactionsWithOrdinaryA,
                            ];
                          }
                          if (
                            ordinaryPersonResponse.data
                              .interactionsWithOrdinaryB
                          ) {
                            ordinaryInteractions = [
                              ...ordinaryInteractions,
                              ...ordinaryPersonResponse.data
                                .interactionsWithOrdinaryB,
                            ];
                          }
                          return (
                            <Text fs={14} fw={400} lh={125} classNames="data">
                              {ordinaryInteractions
                                .map((inter) => inter.name)
                                .join(", ")}
                            </Text>
                          );
                        }}
                      </>
                    </div>
                  )}

                  {ordinaryPersonResponse.data
                    .interactionWithOrdinaryExplanation && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Interactions With Ordinary People (Explanation):
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {
                            ordinaryPersonResponse.data
                              .interactionWithOrdinaryExplanation
                          }
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.interactionsWithUnordinary &&
                    ordinaryPersonResponse.data.interactionsWithUnordinary
                      .length > 0 && (
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Interactions With Notable People:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.interactionsWithUnordinary
                            .map((inter) => inter.name)
                            .join(", ")}
                        </Text>
                      </div>
                    )}

                  {ordinaryPersonResponse.data
                    .interactionWithUnordinaryExplanation && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Interactions With Notable People (Explanation):
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {
                            ordinaryPersonResponse.data
                              .interactionWithUnordinaryExplanation
                          }
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.sources &&
                    ordinaryPersonResponse.data.sources.length > 0 && (
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Source(s):
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.sources
                            .map((inter) => inter.name)
                            .join(", ")}
                        </Text>
                      </div>
                    )}

                  {ordinaryPersonResponse.data.biography && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Biography:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.biography}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.descriptionInTheSource && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Description In The Source(s):
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.descriptionInTheSource}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.explanationOfEthnicity && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Explanation of Ethnicity:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.explanationOfEthnicity}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.location && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Location:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.location.name}
                        </Text>
                      </div>
                    </>
                  )}

                  {ordinaryPersonResponse.data.backgroundCity && (
                    <>
                      <div className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          Background Location:
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryPersonResponse.data.backgroundCity.name}
                        </Text>
                      </div>
                    </>
                  )}
                </div>
              </>
            )
          )}
        </div>
      </div>
    </ReactModal>
  );
};
export default OrdinaryPersonModal;
