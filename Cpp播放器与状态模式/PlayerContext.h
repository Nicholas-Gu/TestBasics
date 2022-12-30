#pragma once

//Ç°ÖÃÉùÃ÷
class IPlayerState;

class PlayerContext 
{
public :
	PlayerContext();
	~PlayerContext();
	void setTargetState(IPlayerState *targetState);
	IPlayerState *getStopState();
	IPlayerState *getPlayState();
	IPlayerState *getPauseState();

	void play();
	void pause();
	void stop();
	
private:
	IPlayerState *m_currentState;
	IPlayerState *m_stopState;
	IPlayerState *m_playState;
	IPlayerState *m_pauseState;
	

};