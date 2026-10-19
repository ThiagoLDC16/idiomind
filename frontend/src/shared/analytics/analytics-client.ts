import { Platform } from 'react-native';

import { env } from '@/shared/config/env';

import type { AnalyticsEventName, AnalyticsEventProperties } from './analytics-events';
import type { Mixpanel as MixpanelClient } from 'mixpanel-react-native';

const MIXPANEL_US_SERVER_URL = 'https://api.mixpanel.com';

const commonProperties = {
  app_environment: env.appEnv,
  platform: Platform.OS,
};

let clientPromise: Promise<MixpanelClient | null> | null = null;
let operationQueue = Promise.resolve();

function logFailure(operation: string, error: unknown) {
  if (__DEV__) {
    console.warn(`[analytics] ${operation} failed:`, error);
  }
}

async function createClient(): Promise<MixpanelClient | null> {
  if (!env.mixpanelToken) return null;

  const { Mixpanel } = await import('mixpanel-react-native');
  const client = new Mixpanel(env.mixpanelToken, false, false);
  client.setUseIpAddressForGeolocation(false);
  await client.init(false, commonProperties, MIXPANEL_US_SERVER_URL);

  return client;
}

function getClient() {
  clientPromise ??= createClient().catch((error) => {
    logFailure('initialization', error);
    return null;
  });

  return clientPromise;
}

function enqueue(
  operationName: string,
  operation: (client: MixpanelClient) => void | Promise<void>,
) {
  operationQueue = operationQueue
    .then(async () => {
      const client = await getClient();
      if (client) await operation(client);
    })
    .catch((error) => {
      logFailure(operationName, error);
    });
}

export const analytics = {
  initialize() {
    enqueue('initialization', () => {});
  },

  track<EventName extends AnalyticsEventName>(
    eventName: EventName,
    properties: AnalyticsEventProperties[EventName],
  ) {
    enqueue(`tracking ${eventName}`, (client) => {
      client.track(eventName, properties);
    });
  },

  identify(userId: string) {
    enqueue('identify', (client) => client.identify(userId));
  },

  reset() {
    enqueue('reset', (client) => {
      client.reset();
    });
  },

  flush() {
    enqueue('flush', (client) => {
      client.flush();
    });
  },
};
