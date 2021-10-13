public interface ICard {
    //Called when the card is played onto a character
    void Played(Character c);
    //Called when the card is executed on the character's turn
    void Resolved();
    //Called when the card is permanently removed from the deck
    void Destroyed();
    //Called when the card is drawn into the hand
    void Drawn();
    //Called when the card is removed from play untill the end of the battle
    void Exiled();
    //Called when the card is sent from the hand to the discard pile
    void Discarded();
    //Called when the card was played onto a character but was cancelled for some reason (stun, etc.)
    void Canceled();

}
