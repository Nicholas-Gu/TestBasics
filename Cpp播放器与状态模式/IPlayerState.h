#pragma once

#include "PlayerContext.h"

class IPlayerState 
{
public:
	IPlayerState(PlayerContext *playerContext) {
		m_playerContext = playerContext;
	}
	virtual void play() = 0;
	virtual void pause() = 0;
	virtual void stop() = 0;
protected:
	PlayerContext* m_playerContext;
};