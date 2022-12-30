#pragma once
#include "IPlayerStatet.h"

class PlayerStatePlay : public IPlayerState
{
public:
	PlayerStatePlay(PlayerContext *playerContext);
	virtual void play();
	virtual void pause();
	virtual void stop();

};